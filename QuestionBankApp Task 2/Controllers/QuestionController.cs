using QuestionBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionBankApp.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        private void DropDownOptions()
        {
            var context = new BlogEntities();
            var propTypes = context.SubjectTable1.ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var prop in propTypes)
            {
                items.Add(new SelectListItem { Text = prop.Subject, Value = prop.subjectId.ToString() });
            }
            ViewBag.SubjectType = items;
        }
        public ActionResult Add()
        {
            DropDownOptions();
            var model = new Question_Table1();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Question_Table1 ques)
        {
            var context = new BlogEntities();
            context.Question_Table1.Add(ques);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult displayAllQuestion()
        {
            var context = new BlogEntities();
            var data = context.Question_Table1.ToList();
            return View(data);
        }

        public ActionResult Edit(string id)
        {
            DropDownOptions();
            var context = new BlogEntities();
            var QID = int.Parse(id);
            var selected = context.Question_Table1.SingleOrDefault(Q => Q.QuestionId == QID);
            return View(selected);
        }


        public ActionResult UpdateQuestion(Question_Table1 ques)
        {
            DropDownOptions();
            var context = new BlogEntities();
            var selected = context.Question_Table1.SingleOrDefault(Q => Q.QuestionId == ques.QuestionId);
            if (selected == null)
            {
                throw new Exception("No data is found");
            }
            selected.Question = ques.Question;
            selected.Keyword = ques.Keyword;
            selected.SubjectType = ques.SubjectType;
            selected.Option1 = ques.Option1;
            selected.Option2 = ques.Option2;
            selected.Option3 = ques.Option3;
            selected.Option4 = ques.Option4;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        /* public ActionResult Delete(string id)
         {
             var context = new BlogEntities();
             var QID = int.Parse(id);
             var selected = context.Question_Table1.SingleOrDefault(Q => Q.QuestionId == QID);
             context.Question_Table1.Remove(selected);
             context.SaveChanges();
             return RedirectToAction("Index");
         }*/

        /*public ActionResult SearchBYKeyword()
        {
            var newRec = new Question_Table1();//Create a Blank object
            return View(newRec);
        }
        [HttpPost]
        public ActionResult SearchBYKeyword(Question_Table1 postedData)
        {
            var context = new BlogEntities();
            var model = context.Question_Table1.Where((qn) => qn.Keyword == postedData.se).FirstOrDefault();
            Searchedquestions.Add(model);
            return View();
        }
        public ActionResult Search(Question_Table1 postedData)
        {
            var context = new MiniProjectEntities1();
            var records = Searchedquestions.ToList();//SELECT * FROM EMPTABLE....
            return View(records);
        }*/
    }
}