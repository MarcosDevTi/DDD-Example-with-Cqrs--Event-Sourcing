﻿using System;
using System.Collections.Generic;
using System.Linq;
using DDDExample.Domain.Core.Event;
using Newtonsoft.Json;

namespace DDDExample.Domain.Event
{
    public class CustomerHistoryHelper
    {
        public static IList<CustomerHistoryData> HistoryData { get; set; }

        public static IReadOnlyList<CustomerHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CustomerHistoryData>();
            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CustomerHistoryData>();
            var last = new CustomerHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CustomerHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                        ? ""
                        : change.BirthDate.Substring(0, 10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CustomerHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CustomerCreated":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerUpdated":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerDeleted":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}
