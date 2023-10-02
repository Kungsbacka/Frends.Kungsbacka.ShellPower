using System;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Newtonsoft.Json.Linq;

namespace Frends.Kungsbacka.ShellPower
{
    /// <summary>
    /// Encapsulates a PowerShell runspace
    /// </summary>
    public class Session : IDisposable
    {
        private readonly Runspace _runspace;
        private bool _disposed;

        /// <summary>
        /// Creates a new PowerShell session (runspace)
        /// </summary>
        public Session()
        {
            _runspace = RunspaceFactory.CreateRunspace();
            _runspace.Open();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("PowerShell runspace is disposed. You must create a new ExchangeSession object.");
            }
        }

        private void ThrowIfNotInitialized()
        {
            if (_runspace == null || _runspace.RunspaceStateInfo.State != RunspaceState.Opened)
            {
                throw new InvalidOperationException("You must call CreateSession() before calling any other method.");
            }
        }

        /// <summary>
        /// Executes a command in the current runspace
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns>{JToken}</returns>
        /// <exception cref="ObjectDisposedException" />
        /// <exception cref="InvalidOperationException" />
        public JToken InvokeCommand(string command, System.Collections.IDictionary parameters)
        {
            ThrowIfDisposed();
            ThrowIfNotInitialized();
            using PowerShell ps = PowerShell.Create();
            ps.Runspace = _runspace;
            ps.AddCommand(command)
                    .AddParameters(parameters)
                    .AddCommand("ConvertTo-Json")
                    .AddParameter("Depth", 10)
                    .AddParameter("Compress")
                    .AddParameter("ErrorAction", "SilentlyContinue");
            var result = ps.Invoke();
            StringBuilder sb = new();
            foreach (PSObject obj in result)
            {
                sb.Append(obj.ToString());
            }
            if (sb.Length > 0)
            {
                return JToken.Parse(sb.ToString());

            }
            return new JObject();
        }

        /// <summary>
        /// IDispose implementation
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_runspace != null)
                    {
                        _runspace.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// IDispose implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
