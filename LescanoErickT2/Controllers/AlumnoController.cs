using Newtonsoft.Json;
using LescanoErickT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LescanoErickT2.Controllers
{
    public class AlumnoController : Controller
    {

        static string jListAlumnos = @"[]";

        static List<Alumno> listaAlumnos = new List<Alumno>();
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                List<Alumno> Listaintermedia = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
                return View(Listaintermedia);
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View();
        }

        [HttpGet]

        public ActionResult Create() {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Alumno alumno) {

            try
            {
                List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);

                if (listatemporal.Any(a => a.dni == alumno.dni))
                {
                    ModelState.AddModelError("dni", "Ya existe un alumno con ese DNI");
                    return View(alumno);
                }

                listatemporal.Add(alumno);
                jListAlumnos = JsonConvert.SerializeObject(listatemporal);
                ViewBag.mensaje = "Alumno generado correctamente";
                return View(alumno);

            }
            catch (JsonException ex) {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int dni) {
            List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            Alumno alumnoencontrado = listatemporal.Find(a => a.dni == dni);
                return View(alumnoencontrado);
        }


        [HttpPost]
        public ActionResult Delete(Alumno alumno)
        {
            try
            {
                List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
                int index = listatemporal.FindIndex(a => a.dni == alumno.dni);
                if (index >= 0)
                {
                    listatemporal.RemoveAt(index);
                }
                jListAlumnos = JsonConvert.SerializeObject(listatemporal);
                return RedirectToAction("Index");

            }
            catch (JsonException ex) {
                ViewBag.mensaje = ex.Message;
                return View(alumno);
            }
        }

        public ActionResult Details(int dni) {
            List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            Alumno alumnoencontrado = listatemporal.Find(a => a.dni == dni);
            return View(alumnoencontrado);
        }

        [HttpGet]
        public ActionResult Edit(int dni) {

            List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            Alumno alumnoencontrado = listatemporal.Find(a => a.dni == dni);
            return View(alumnoencontrado);

        }


        [HttpPost]
        public ActionResult Edit(Alumno alumno, int dniOriginal)
        {
            try
            {
                List<Alumno> listatemporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
                if (listatemporal.Any(a => a.dni == alumno.dni && a.dni != dniOriginal))
                {
                    ModelState.AddModelError("dni", "Ya existe otro alumno con ese DNI");
                    return View(alumno);
                }

                int index = listatemporal.FindIndex(a => a.dni == dniOriginal);
                if (index < 0)
                {
                    ViewBag.mensaje = "No se encontró el alumno";
                    return View(alumno);
                }


                listatemporal[index] = alumno;
                jListAlumnos = JsonConvert.SerializeObject(listatemporal);

                ViewBag.mensaje = "Alumno editado correctamente";
                return View(alumno);

            }
            catch (JsonException ex) {
                ViewBag.mensaje = ex.Message;
                return View();

            }

        }
    }
}