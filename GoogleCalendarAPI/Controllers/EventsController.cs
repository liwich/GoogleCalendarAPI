using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Calendar.v3;
using GoogleCalendarAPI.GoogleUtils;
using System.Threading.Tasks;
using System.Threading;
using Google.Apis.Calendar.v3.Data;

namespace GoogleCalendarAPI.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public async Task<ActionResult> Index(string id,CancellationToken cancellationToken)
        {
            var request = await AppFlowMetadata.Authenticate(this, cancellationToken);

            if (request.GetType().Equals(typeof(CalendarService)))
            {
                CalendarService service = (CalendarService)request;

                Events list = service.Events.List("luismrodram@gmail.com").Execute();
                ViewBag.Id = id;
                ViewBag.EventsList = list.Items;

                return View();
            }
            else
            {
                return (RedirectResult)request;
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public async Task<ActionResult> Edit(Event evt, CancellationToken cancellationToken)
        {
            var request = await AppFlowMetadata.Authenticate(this, cancellationToken);

            if (request.GetType().Equals(typeof(CalendarService)))
            {
                CalendarService service = (CalendarService)request;

                Event responseEvent = service.Events.Get("luismrodram@gmail.com", evt.Id).Execute();
                ViewBag.evt = responseEvent;
                ViewBag.Id = evt.Id;

                return View();
            }
            else
            {
                return (RedirectResult)request;
            }
            
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
