﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StationLogWebApplication1;
using StationLogWebApplication1.Models;

namespace StationLogWebApplication1.Controllers
{
    public class StationsController : ApiController
    {
        private StationLogModel db = new StationLogModel();

        // GET: api/Stations
        public IQueryable<Station> GetStations()
        {
            return db.Stations;
        }

        // GET: api/Stations/5
        [ResponseType(typeof(Station))]
        public IHttpActionResult GetStation(int id)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        // PUT: api/Stations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStation(int id, Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != station.StationId)
            {
                return BadRequest();
            }

            db.Entry(station).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stations
        [ResponseType(typeof(Station))]
        public IHttpActionResult PostStation(Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stations.Add(station);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StationExists(station.StationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = station.StationId }, station);
        }

        // DELETE: api/Stations/5
        [ResponseType(typeof(Station))]
        public IHttpActionResult DeleteStation(int id)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }

            db.Stations.Remove(station);
            db.SaveChanges();

            return Ok(station);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StationExists(int id)
        {
            return db.Stations.Count(e => e.StationId == id) > 0;
        }
    }
}