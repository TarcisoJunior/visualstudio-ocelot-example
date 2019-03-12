using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeoCoordinatePortable;


namespace OcelotGatewayExample.Controllers
{

    public class JsonDistance : JsonResult
    {
        GeoCoordinate coordinate1;
        GeoCoordinate coordinate2;

        public JsonDistance(object value) : base(value)
        {
        }

        public JsonDistance(GeoCoordinate c1, GeoCoordinate c2) : base(c1)
        {
            this.coordinate1 = c1;
            this.coordinate2 = c2;
        }

        //public JsonDistance(GeoCoordinate c1, GeoCoordinate c2)
        //{
        //    this.coordinate1 = c1;
        //    this.coordinate2 = c2;
        //}

        public double getDistance()
        {
            return this.coordinate1.GetDistanceTo(this.coordinate2);
        }

        public string getDistanceJsonObject()
        {
            String joText = String.Format("{{ coordinate1: {0}, \n coordinate2: {1}, \n distance: {2} \n }}", coordinate1.ToString(), coordinate2.ToString(), getDistance());
            return joText;
            //return String.Format("A Distancia entre {0} e {1} é {2} metros", this.coordinate1, this.coordinate2, getDistance());

        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // GET api/values/soma/{vr1}/{vr2}
        [HttpGet("soma/{vr1}/{vr2}")]
        //[HttpGet("{vr2}")]
        public ActionResult<string> Get(int vr1, int vr2)
        {
            int soma = vr1 + vr2;
            return string.Format("A soma de {0} mais {1} = {2} " , vr1, vr2, soma);
        }

        [HttpGet("multi/{vr1}/{vr2}")]
        public ActionResult<double> Get(double vr1, double vr2) 
        {
            double result = vr1 * vr2;
            return result;
         }

        [HttpGet("distance/{lat1}/{lng1}/{lat2}/{lng2}")]
        public ActionResult<double> Get(double lat1, double lng1, double lat2, double lng2)
        {
            GeoCoordinate coordinate1 = new GeoCoordinate(lat1, lng1);
            GeoCoordinate coordinate2 = new GeoCoordinate(lat2, lng2);

            //Double distanceBetween = coordinate1.GetDistanceTo(coordinate2);

            JsonDistance jr = new JsonDistance(coordinate1, coordinate2);
            return jr.getDistance();
        }

        [HttpGet("distance/json/{lat1}/{lng1}/{lat2}/{lng2}")]
        public ActionResult<string> GetJsonObject(double lat1, double lng1, double lat2, double lng2)
        {
            GeoCoordinate coordinate1 = new GeoCoordinate(lat1, lng1);
            GeoCoordinate coordinate2 = new GeoCoordinate(lat2, lng2);

            //Double distanceBetween = coordinate1.GetDistanceTo(coordinate2);

            JsonDistance jr = new JsonDistance(coordinate1, coordinate2);
            return jr.getDistanceJsonObject();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
