using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace MVC.MyClones.Hubs
{
    public class TestHub : Hub
    {
        IFieldService _fieldService;
        Timer _timer;

        public TestHub(IFieldService fieldService)
        {
            _fieldService = fieldService;
            _timer = new Timer(2000);
            _timer.Elapsed += (s, e) =>
            {
                Clients.All.SendCoreAsync("OnFieldsChanged", new[] { _fieldService.GetFields() });
            };

            _timer.Start();
        }
    }
}
