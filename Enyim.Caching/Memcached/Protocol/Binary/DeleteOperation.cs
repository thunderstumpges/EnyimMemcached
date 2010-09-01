using System.Collections.Generic;
using System.Text;

namespace Enyim.Caching.Memcached.Protocol.Binary
{
	public class DeleteOperation : BinarySingleItemOperation, IDeleteOperation
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DeleteOperation));

		public DeleteOperation(string key) : base(key) { }

		protected override BinaryRequest Build()
		{
			var request = new BinaryRequest(OpCode.Delete)
			{
				Key = this.Key,
				Cas = this.Cas
			};

			return request;
		}

		protected internal override bool ReadResponse(PooledSocket socket)
		{
			var response = new BinaryResponse();
			var retval = response.Read(socket);
			this.Cas = response.CAS;

			if (!retval)
				if (log.IsDebugEnabled)
					log.DebugFormat("Delete failed for key '{0}'. Reason: {1}", this.Key, Encoding.ASCII.GetString(response.Data.Array, response.Data.Offset, response.Data.Count));

			return retval;
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kisk�, enyim.com
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion
