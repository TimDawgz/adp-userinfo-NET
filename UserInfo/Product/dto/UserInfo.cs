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

using System.Runtime.Serialization;


namespace ADPClient.Product.dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string sub { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string organizationOID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string associateID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string givenName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string familyName { set; get; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        public string picture { get; set; }
    }
}
