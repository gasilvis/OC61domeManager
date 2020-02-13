using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TA.Ascom.ReactiveCommunications;
using TA.Ascom.ReactiveCommunications.Transactions;

namespace reactiveDemo
{
    class Program
    {
        static void Main(string[] args)
        { 
            var log = LogManager.GetCurrentClassLogger();
            var connectionString = "COM3:9600";
            var factory = new ChannelFactory();
            var channel = factory.FromConnectionString (connectionString);

            var observer = new TransactionObserver(channel);
            var processor = new ReactiveTransactionProcessor();
            processor.SubscribeTransactionObserver(observer);

            channel.Open(); // normmaly done in drivers Connected property

            var transaction = new TerminatedStringTransaction("*OPEN_MIRROR_COVER\r", initiator:'*', terminator: '\r'); // ":GR#"
            log.Debug($"Created transaction: {transaction}");
            processor.CommitTransaction(transaction);
            transaction.WaitForCompletionOrTimeout();
            log.Debug($"Finished transaction: {transaction}");

            if(transaction.Successful && transaction.Response.Any())
            {
                var response = transaction.Response.Single();
                log.Info($"received response: {response}");
            }

            Console.WriteLine("Press enter..");
            Console.Read();
        }
    }
}
