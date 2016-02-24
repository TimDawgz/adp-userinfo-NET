/*# This file is part of adp-api-library.
# https://github.com/adp/adp-api-lib-net

# Copyright © 2015-2016 ADP, LLC.

# Licensed under the Apache License, Version 2.0 (the “License”);
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at

# http://www.apache.org/licenses/LICENSE-2.0

# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an “AS IS” BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied.  See the License for the specific language
# governing permissions and limitations under the License.
*/
using System;
using ADPClient.Product.dto;


namespace ADPClient.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoHelper : IAPIProductHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="urlmap"></param>
        public UserInfoHelper(ADPApiConnection connection, string[] urlmap=null)
        {
            if (connection == null)
            {
                throw new Exception("Invalid connection: Null");
            }

            Connection = connection;
            UrlMap = urlmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserInfo getUserInfo()
        {
            string productjsondata = null;

            if (!Connection.isConnectedIndicator())
            {
                throw new Exception("Not connected.");
            }
            productjsondata = Connection.getADPData(producturl);
            return JSONUtil.Deserialize<UserInfo>(productjsondata);
        }

        /// <summary>
        /// ADP url for UserInfo API product
        /// </summary>
        private String producturl = "https://iat-api.adp.com/core/v1/userinfo";
    }
}
