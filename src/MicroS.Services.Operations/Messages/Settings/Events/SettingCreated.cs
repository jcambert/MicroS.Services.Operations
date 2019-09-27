﻿using MicroS_Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroS.Services.Operations.Messages.Settings.Events
{
    [MessageNamespace("settings")]
    public class SettingCreated : IEvent
    {
        public SettingCreated(Guid id,
           int numero,
           string description,
           string type,
           string stringValue,
           int? intValue,
           double? doubleValue,
           DateTime? dtValue)
        {
            Id = id;
            Numero = numero;
            Description = description;
            Type = type;
            StringValue = stringValue;
            IntegerValue = intValue;
            DoubleValue = doubleValue;
            DateValue = dtValue;
        }

        public Guid Id{get;}

        public int Numero { get; }

        public  string Description { get; }

        public  string Type { get; }

        public  string StringValue { get; }

        public  int? IntegerValue { get; }

        public  double? DoubleValue { get; }

        public  DateTime? DateValue { get; }
    }
}
