﻿using ASPDbContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetUpMapsHere.Services
{
    public class OSMService : IOSMService
    {
        private readonly TransportDbContext db;
        public OSMService(TransportDbContext db)
        {
            this.db = db;
        }

        public string GetAllRoutes(params string[] lines)
        {
            string defaultColor = "#808080";
            if (lines.Length != 0)
            {
                var routes = db.BusLines.Where(x => lines.Contains(x.Name)).Select(x => new
                {
                    Name = x.Name,
                    ColorHex = String.IsNullOrEmpty(x.ColorHex) ? defaultColor : x.ColorHex,
                    coordinates = x.Route.OrderBy(y => y.RowPosition).Select(y => new double[] { y.Point.X, y.Point.Y }),
                });
                return JsonConvert.SerializeObject(routes);
            }
            else
            {
                var routes = db.BusLines.Select(x => new
                {
                    Name = x.Name,
                    ColorHex = String.IsNullOrEmpty(x.ColorHex) ? defaultColor : x.ColorHex,
                    coordinates = x.Route.OrderBy(y => y.RowPosition).Select(y => new double[] { y.Point.X, y.Point.Y }),
                });
                return JsonConvert.SerializeObject(routes);
            }
        }

        public string GetAllStops(params string[] lines)
        {
            if (lines.Length != 0)
            {
                var stops = db.Stops.Where(x => x.LineStops.Any(y => lines.Contains(y.Line.Name))).Select(x => new //remodel database so that linepoints are not busStopPoints
                {
                    StopList = string.Join(", ", x.LineStops.Where(y => lines.Contains(y.Line.Name)).Select(y => y.Line.Name)),
                    coordinates = new double[] { x.Point.X, x.Point.Y }
                });
                return JsonConvert.SerializeObject(stops);
            }
            else
            {
                var stops = db.Stops.Select(x => new //remodel database so that linepoints are not busStopPoints
                {
                    StopList = string.Join(", ", x.LineStops.Select(y => y.Line.Name)),
                    coordinates = new double[] { x.Point.X, x.Point.Y }
                });
                return JsonConvert.SerializeObject(stops);

            }
        }
    }
}