using System;
using Neo.Persistence;
using Neo.Network.P2P.Payloads;
using Neo.VM;
using Neo;
using Neo.SmartContract;
using Neo.Persistence.LevelDB;
using Neo.IO.Json;
using System.Collections.Generic;

namespace NEOONENodeVM
{
    class Program
    {
        static void Main(string arg)
        {
            // input argument is a stringified JSON object
            // This needs to be Neo 3 version
            JObject jsonArg = JObject.Parse(arg);

            // create new invocation transaction with input args
            InvocationTransaction tx_invocation = new InvocationTransaction
            {
                Version = 1,
                Script = new byte[0],
                Gas = new Fixed8(),
                Attributes = new TransactionAttribute[0],
                Inputs = new CoinReference[0],
                Outputs = new TransactionOutput[0]
            };
            // can we use a dummy snapshot?
            Snapshot snapshot = new LevelDBStore("").GetSnapshot();
            long gasIn = Convert.ToInt64("10.3423"); // replace with GAS argument
            Fixed8 gas = new Fixed8(gasIn);
            ApplicationEngine engine = new ApplicationEngine(TriggerType.Application, tx_invocation, snapshot, gas);
            engine.LoadScript(tx_invocation.Script);
            engine.Execute();
            if (!engine.State.HasFlag(VMState.FAULT))
            {
                engine.Service.Commit();
            }

            string trigger = TriggerType.Application.ToString();
            string scriptHash = tx_invocation.Script.ToScriptHash().ToString();
            string engineState = engine.State.ToString();
            string gasConsumed = engine.GasConsumed.ToString();
            string[] notifications = engine.Service.Notifications.ToArray();
            string[] resultStack = ResultStackToStringArray(engine.ResultStack);


            // output can be stringified JSON object or deliminated results
            // does JObject have stringify?

            Console.WriteLine($"{trigger}${scriptHash}${engineState}${gasConsumed}${resultStack}${notifications}");
        }

        private string[] ResultStackToStringArray(RandomAccessStack<StackItem> stack)
        {
            int stackSize = stack.Count;
            List<string> stringList = new List<string>();
            for (int i = 0; i < stackSize; i++)
            {
                list.Add(stack.Pop().ToString());
            }

            // Index 0 is the "top" of the stack
            return stringList.ToArray();
        }
    }
}
