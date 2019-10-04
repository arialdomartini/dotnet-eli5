using System;
using System.ServiceModel;
using System.ServiceModel.Description;


namespace Library2_472_newcsproj
{
    public class MyService : IMyService
    {
        public string SayHello(string name)
        {
            return $"Hello, {name}, I'm a WCF service and I've been invoked";
        }
    }

    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string SayHello(String name);
    }

    public class WcfCall
    {
        public static string InvokeIt()
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            var address = new Uri ("net.pipe://localhost/sample/hello");
            var host = new ServiceHost (typeof(MyService));

            var behavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            behavior.IncludeExceptionDetailInFaults = true;

            host.AddServiceEndpoint (typeof(IMyService), binding, address);
            host.Open ();

            var channel = new ChannelFactory<IMyService>(binding, new EndpointAddress(address)).CreateChannel();

            var result = channel.SayHello("world");

            host.Close();

            return result;
        }

    }
}