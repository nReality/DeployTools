﻿using CommandLine;
using Org.DeployTools.Shared.CommandLineOptions;
using System;

namespace Org.DeployTools.ServiceControl
{
    class Options : ICommandLineOptions
    {
        [Option("servername", HelpText = "Name of the server to connect to", Required = false)]
        public string Servername { get; set; }
        [ValueOption(0)]
        public ScAction Action { get; set; }
        [ValueOption(1)]
        public string ServiceName { get; set; }

        public enum ScAction
        {
            Unknown,
            Stop,
            Start,
        }

        public void GuardArgumentsValid()
        {
            if (Action == ScAction.Unknown)
                throw new ArgumentException("Action not specifed (first unnamed argument)");
            if (string.IsNullOrEmpty(ServiceName))
                throw new ArgumentException("ServiceName not specifed (second unnamed argument)");
        }

        public void Setup()
        {
            if (!string.IsNullOrEmpty(Servername))
                Servername = @"\\" + Servername;
        }
    }
}