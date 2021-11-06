using CrimsonCurrency.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace CrimsonCurrency.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrimsonCurruncyController : ControllerBase
    {
        // can be as singleton
        public Blockchain bc { get; set; }
        public CrimsonCurruncyController()
        {
            bc = new Blockchain();
        }

        [HttpGet]
        public ActionResult Get() => Ok(bc);


        [HttpPost]
        [Route("mine")]
        public ActionResult MineBlock([FromBody] Dataholder data)
        {
            if (data == null) return BadRequest("Invalid Block");
            var blck = bc.AddBlock(data);
            return Ok(bc);

        }

        [HttpGet("wc")]
       
        public async Task OpenSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                                   HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        // change location later after u understand it 
        // can take all sorts of data types it's nice !
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}

