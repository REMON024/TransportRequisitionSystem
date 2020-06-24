/* 
 Created By : Md. Nahid Hasan
 Created Date : 27-11-2018
 */

namespace NybSys.Mqtt
{
    public class MQTTConnector
    {
        public string MQBrokerHost { get; set; }
        public int MQbrokerPort { get; set; }
        public string MQUserID { get; set; }
        public string MQPassword { get; set; }
        public string ClientId { get; set; }
        public int AlivePeriod { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
