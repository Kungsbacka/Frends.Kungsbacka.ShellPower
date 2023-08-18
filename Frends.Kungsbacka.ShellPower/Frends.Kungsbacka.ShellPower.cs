using System.Collections;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

#pragma warning disable 1591

namespace Frends.Kungsbacka.ShellPower
{
    public static class PS
    {

        public static CreateSessionResult CreateSession()
        {
            Session session = new();
            return new CreateSessionResult()
            {
                Session = session
            };
        }

        public static RemoveSessionResult RemoveSession(RemoveSessionParameters input)
        {
            if (input != null && input.Session != null)
            {
                input.Session.Dispose();
                input.Session = null;
            }
            return new RemoveSessionResult();
        }

        public static InvokeCommandResult InvokeCommand(InvokeCommandParameters input, [PropertyTab] InvokeCommandOptions options)
        {
            IDictionary parameters = new Hashtable(input.Parameters.Length);
            foreach (CommandParameter param in input.Parameters)
            {
                parameters[param.Name] = param.Value;
            }
            JToken result;
            if (options.Session != null)
            {
                result = options.Session.InvokeCommand(input.Command, parameters);
            }
            else
            {
                using Session session = new();
                result = session.InvokeCommand(input.Command, parameters);
            }
            return new InvokeCommandResult() { Result = result };
        }
    }
}
