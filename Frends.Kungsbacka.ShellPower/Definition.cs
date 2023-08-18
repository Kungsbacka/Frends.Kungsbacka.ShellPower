using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Kungsbacka.ShellPower
{
    /// <summary>
    /// Required parameters for InvokeCommand task
    /// </summary>
    public class InvokeCommandParameters
    {
        /// <summary>
        /// PowerShell command
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Command { get; set; }

        /// <summary>
        /// Command parameters
        /// </summary>
        public CommandParameter[] Parameters { get; set; }
    }

    /// <summary>
    /// Optional parameters for InvokeCommand task
    /// </summary>
    public class InvokeCommandOptions
    {
        /// <summary>
        /// Optional PowerShell session object where the command is run
        /// </summary>
        [DisplayFormat(DataFormatString = "Expression")]
        public Session Session { get; set; }
    }

    /// <summary>
    /// PowerShell command parameter
    /// </summary>
    public class CommandParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Name { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public object Value { get; set; }
    }

    /// <summary>
    /// InvokeCommand task result
    /// </summary>
    public class InvokeCommandResult
    {
        /// <summary>
        /// Result
        /// </summary>
        public JToken Result { get; set; }
    }

    /// <summary>
    /// CreateSession task result
    /// </summary>
    public class CreateSessionResult
    {
        /// <summary>
        /// PowerShell session
        /// </summary>
        public Session Session { get; set; }
    }

    /// <summary>
    /// Required parameters RemoveSession task
    /// </summary>
    public class RemoveSessionParameters
    {
        /// <summary>
        /// PowerShell session
        /// </summary>
        public Session Session { get; set; }
    }

    /// <summary>
    /// RemoveSession does not give a result, but frends
    /// does not accept a task that returns void.
    /// </summary>
    public class RemoveSessionResult { }

    ///// <summary>
    ///// Parameters class usually contains parameters that are required.
    ///// </summary>
    //public class Parameters
    //{
    //    /// <summary>
    //    /// Something that will be repeated.
    //    /// </summary>
    //    [DisplayFormat(DataFormatString = "Text")]
    //    [DefaultValue("Lorem ipsum dolor sit amet.")]
    //    public string Message { get; set; }
    //}

    ///// <summary>
    ///// Options class provides additional optional parameters.
    ///// </summary>
    //public class Options
    //{
    //    /// <summary>
    //    /// Number of times input is repeated.
    //    /// </summary>
    //    [DefaultValue(3)]
    //    public int Amount { get; set; }

    //    /// <summary>
    //    /// How repeats of the input are separated.
    //    /// </summary>
    //    [DisplayFormat(DataFormatString = "Text")]
    //    [DefaultValue(" ")]
    //    public string Delimiter { get; set; }
    //}

    //public class Result
    //{
    //    /// <summary>
    //    /// Contains the input repeated the specified number of times.
    //    /// </summary>
    //    [DisplayFormat(DataFormatString = "Text")]
    //    public string Replication;
    //}
}
