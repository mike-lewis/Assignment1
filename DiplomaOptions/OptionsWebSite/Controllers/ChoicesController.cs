using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.DataContext;
using DiplomaDataModel.OptionPicker;
using OptionsWebSite.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private OptionPickerContext db = new OptionPickerContext();

        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            return View(choices.ToList());
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId");
            ViewBag.StudentId = user.UserName;
            //ViewBag.YearTerm = termAsString;
            return View(choice);
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            var options = from a in db.Options
                          where a.IsActive.Equals(true)
                          select a;
            var query = from b in db.YearTerms
                        where b.IsDefault.Equals(true)
                        select b;
            var term = query.FirstOrDefault();
            var termAsString = "";

            if (term.Term == 10)
            {
                termAsString = term.Year.ToString() + " Winter";
            } else if(term.Term == 20)
            {
                termAsString = term.Year.ToString() + " Spring/Summer";
            } else if(term.Term == 30)
            {
                termAsString = term.Year.ToString() + " Fall";
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId");
            ViewBag.StudentId = user.UserName;
            ViewBag.YearTerm = termAsString;
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            // get year term id
            var year = from y in db.YearTerms
                       where y.IsDefault.Equals(true)
                       select y;
            var t = year.FirstOrDefault();
            var yearTerm = t.YearTermId;
            choice.YearTermId = yearTerm;

            var isDistinct = from a in db.Choices
                             where a.YearTermId.Equals(choice.YearTermId)
                             where a.StudentId.Equals(choice.StudentId)
                             select a;
            var any = from b in db.Choices
                      select b;

            if ((!isDistinct.Any()) && (any.Any()))
            {
                ModelState.AddModelError(string.Empty, "You can only choose options once per term.");
            }

            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var query = from b in db.YearTerms
                        where b.IsDefault.Equals(true)
                        select b;
            var term = query.FirstOrDefault();
            var termAsString = "";

            if (term.Term == 10)
            {
                termAsString = term.Year.ToString() + " Winter";
            }
            else if (term.Term == 20)
            {
                termAsString = term.Year.ToString() + " Spring/Summer";
            }
            else if (term.Term == 30)
            {
                termAsString = term.Year.ToString() + " Fall";
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.YearTerm = termAsString;
            ViewBag.StudentId = user.UserName;
            return View(choice);
        }

        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }

            var query = from b in db.YearTerms
                        where b.IsDefault.Equals(true)
                        select b;
            var term = query.FirstOrDefault();
            var termAsString = "";

            if (term.Term == 10)
            {
                termAsString = term.Year.ToString() + " Winter";
            }
            else if (term.Term == 20)
            {
                termAsString = term.Year.ToString() + " Spring/Summer";
            }
            else if (term.Term == 30)
            {
                termAsString = term.Year.ToString() + " Fall";
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            ViewBag.StudentId = user.UserName;
            ViewBag.YearTerm = termAsString;
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
