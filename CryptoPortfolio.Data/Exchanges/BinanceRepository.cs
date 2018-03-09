﻿using CryptoPortfolio.Business.Entities;
using CryptoPortfolio.Business.Entities.Binance;
using CryptoPortfolio.Business.Helper;
using CryptoPortfolio.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolio.Data
{
    public class BinanceRepository : IBinanceRepository
    {
        private Security security;
        private IRESTRepository _restRepo;
        private string baseUrl;
        private ApiInformation _apiInfo;
        //private string apiKey;
        //private string apiSecret;

        public BinanceRepository()
        {
            security = new Security();
            _restRepo = new RESTRepository();
            baseUrl = "https://api.binance.com";
            _apiInfo = new ApiInformation();
            //apiKey = "oUGH1ZcOB7hx7TMmyBeAEOpP9ROFdlbN81hTAIdxzSpwlDgpEMVQfiq0IL2K1RDm";
            //apiSecret = "VjnpS6UFjQ0WCUUmuqmMZSVAdY0M1pzCXija072Db3v5QsBn9q0N4OKzbdTuHYUq";
        }

        public bool SetExchangeApi(ApiInformation apiInfo)
        {
            _apiInfo = apiInfo;
            return true;
        }

        public async Task<Transaction> GetTransactions()
        {
            string url = CreateUrl("/api/v3/allOrders");
            
            var response = await _restRepo.GetApi<Transaction>(url, GetRequestHeaders());

            return response;
        }

        public async Task<Account> GetBalance()
        {
            string url = CreateUrl("/api/v3/account");
            
            var response = await _restRepo.GetApi<Account>(url, GetRequestHeaders());

            return response;
        }

        public async Task<IEnumerable<BinanceTick>> GetCrytpos()
        {
            string url = "v1/open/tick";

            var response = await _restRepo.GetApi<List<BinanceTick>>(url);

            return response;
        }

        public long GetBinanceTime()
        {
            string url = CreateUrl("/api/v1/time", false);

            var response = _restRepo.GetApi<ServerTime>(url);

            response.Wait();

            return response.Result.serverTime;
        }

        private Dictionary<string, string> GetRequestHeaders()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("X-MBX-APIKEY", _apiInfo.apiKey);

            return headers;
        }

        private string CreateUrl(string apiPath, bool secure = true)
        {
            if (!secure)
            {
                return baseUrl + $"{apiPath}";
            }
            var timestamp = GetBinanceTime().ToString();
            var queryString = $"timestamp={timestamp}";
            var hmac = security.GetHMACSignature(_apiInfo.apiSecret, queryString);

            return baseUrl + $"{apiPath}?{queryString}&signature={hmac}";
        }
    }
}
