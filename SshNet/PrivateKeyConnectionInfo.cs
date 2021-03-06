﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Renci.SshNet
{
    /// <summary>
    /// Provides connection information when private key authentication method is used
    /// </summary>
    public class PrivateKeyConnectionInfo : ConnectionInfo, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeyConnectionInfo"/> class.
        /// </summary>
        /// <param name="host">Connection host.</param>
        /// <param name="username">Connection username.</param>
        /// <param name="keyFiles">Connection key files.</param>
        public PrivateKeyConnectionInfo(string host, Lazy<string> username, Lazy<PrivateKeyAgent> privateKeyAgent)
            : this(host, 22, username, privateKeyAgent)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeyConnectionInfo"/> class.
        /// </summary>
        /// <param name="host">Connection host.</param>
        /// <param name="port">Connection port.</param>
        /// <param name="username">Connection username.</param>
        /// <param name="keyFiles">Connection key files.</param>
        public PrivateKeyConnectionInfo(string host, int port, Lazy<string> username, Lazy<PrivateKeyAgent> privateKeyAgent)
            : base(host, port, username, new PrivateKeyAuthenticationMethod(username, privateKeyAgent))
        {
        }

        #region IDisposable Members

        private bool isDisposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.isDisposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (this.AuthenticationMethods != null)
                    {
                        foreach (var authenticationMethods in this.AuthenticationMethods.OfType<IDisposable>())
                        {
                            authenticationMethods.Dispose();
                        }
                    }
                }

                // Note disposing has been done.
                isDisposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PasswordConnectionInfo"/> is reclaimed by garbage collection.
        /// </summary>
        ~PrivateKeyConnectionInfo()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}
