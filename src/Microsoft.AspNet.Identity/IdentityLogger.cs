// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.CompilerServices;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Identity
{
    public class IdentityLogger
    {
        public ILogger Logger { get; set; }

        public virtual TResult LogResult<TResult>(TResult result, Func<TResult, LogLevel> getLevel,
            Func<string> messageAccessor)
        {
            var logLevel = getLevel(result);

            // Check if log level is enabled before creating the message.
            if (Logger.IsEnabled(logLevel))
            {
                Logger.Log(logLevel, 0, messageAccessor(), null, (msg, exp) => (string)msg);
            }

            return result;
        }

        public virtual SignInResult LogSignInResult(SignInResult result, [CallerMemberName]string methodName = null)
           => LogResult(result, r => r.GetLogLevel(), () => Resources.FormatLoggingSigninResult(methodName, result));

        public virtual IdentityResult LogIdentityResult(IdentityResult result, [CallerMemberName]string methodName = null)
            => LogResult(result, r => r.GetLogLevel(), () => Resources.FormatLoggingIdentityResult(methodName, result));

        public virtual bool LogResult(bool result, [CallerMemberName]string methodName = null)
            => LogResult<bool>(result, (b) => b ? LogLevel.Verbose : LogLevel.Warning,
                               () => Resources.FormatLoggingIdentityResult(methodName, result));

    }
}