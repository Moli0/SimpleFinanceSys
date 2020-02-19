using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class SettingController : BaseController
    {
        //
        // GET: /Setting/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsumptionTypes() {
            return View();
        }

        public ActionResult ConsumptionObjects() {
            return View();
        }

        [HttpGet]
        public ActionResult GetType(string id) {
            DAL.Base_TypeDAL dal= new DAL.Base_TypeDAL();
            var user = GetUser();
            Model.Base_TypeModel model = dal.GetDataForType<Model.Base_TypeModel>(id,"Base_Type");
            if (model == null) {
                return Error("未找到相关数据");
            }
            if (model.create_user != user.id) {
                return Error("未找到相关数据");
            }
            return Success(model);
        }

        [HttpGet]
        public ActionResult GetObject(string id)
        {
            DAL.Base_ObjectDAL dal = new DAL.Base_ObjectDAL();
            var user = GetUser();
            Model.Base_ObjectModel model = dal.GetDataForType<Model.Base_ObjectModel>(id, "Base_Object");
            if (model == null)
            {
                return Error("未找到相关数据");
            }
            if (model.create_user != user.id)
            {
                return Error("未找到相关数据");
            }
            return Success(model);
        }

        [HttpGet]
        public ActionResult GetTypes() {
            try
            {
                var user = GetUser();
                if (user == null)
                {
                    return Error("登录已过期，请重新登录！");
                }
                DAL.Base_TypeDAL dal = new DAL.Base_TypeDAL();
                List<Model.Base_TypeModel> models = new List<Model.Base_TypeModel>();
                models = dal.GetListModel(string.Format(" create_user = '{0}' order by sort asc, create_time desc ", user.id));
                return Success(Newtonsoft.Json.JsonConvert.SerializeObject(models));

            }
            catch {
                return Error("数据异常，请稍后再试");
            }
        }

        [HttpGet]
        public ActionResult GetObjects() {
            try
            {
                var user = GetUser();
                if (user == null)
                {
                    return Error("登录已过期，请重新登录！");
                }
                List<Model.Base_ObjectModel> models = new List<Model.Base_ObjectModel>();
                DAL.Base_ObjectDAL dal = new DAL.Base_ObjectDAL();
                models = dal.GetListModel(string.Format(" create_user = '{0}' order by sort asc, create_time desc ", user.id));
                return Success(Newtonsoft.Json.JsonConvert.SerializeObject(models));
            }
            catch
            {
                return Error("数据异常，请稍后再试");
            }
        }

        [HttpPost]
        public ActionResult AddType(string name,int sort ) {
            var user = GetUser();
            if (user == null) {
                return Error("登录过期，请重新登录！");
            }
            Model.Base_TypeModel model = new Model.Base_TypeModel();
            DAL.Base_TypeDAL dal = new DAL.Base_TypeDAL();
            model.Create();
            model.create_user = user.id;
            model.name = name;
            model.sort = sort;
            int res = dal.InsertSql<Model.Base_TypeModel>(model, "base_Type");
            if (res > 0)
            {
                return SuccessMsg("添加成功");
            }
            else {
                return Error("网络繁忙，请稍后再试");
            }
            
        }

        [HttpPost]
        public ActionResult AddObject(string name, string explain,int sort)
        {
            var user = GetUser();
            if (user == null)
            {
                return Error("登录过期，请重新登录！");
            }
            Model.Base_ObjectModel model = new Model.Base_ObjectModel();
            DAL.Base_ObjectDAL dal = new DAL.Base_ObjectDAL();
            model.Create();
            model.create_user = user.id;
            model.name = name;
            model.explain = explain;
            model.sort = sort;
            int res = dal.InsertSql<Model.Base_ObjectModel>(model, "Base_Object");
            if (res > 0)
            {
                return SuccessMsg("添加成功");
            }
            else
            {
                return Error("网络繁忙，请稍后再试");
            }

        }

        [HttpPost]
        public ActionResult UpdateType(string id,string name,int sort) {
            Model.Base_TypeModel model = new Model.Base_TypeModel();
            DAL.Base_TypeDAL dal = new DAL.Base_TypeDAL();
            model = dal.GetDataForType<Model.Base_TypeModel>(id, "Base_Type");
            if (model.create_user != GetUser().id) {
                return Error("未找到相关数据，请刷新后再试");
            }
            model.name = name;
            model.sort = sort;
            model.Modify();
            model.last_user = GetUser().id;
            int res = dal.Update<Model.Base_TypeModel>(model, "Base_Type");
            if (res > 0)
            {
                return SuccessMsg("修改成功");
            }
            else {
                return Error("系统异常，请稍后再试");
            }

        }

        [HttpPost]
        public ActionResult UpdateObject(string id, string name, string explain, int sort)
        {
            Model.Base_ObjectModel model = new Model.Base_ObjectModel();
            DAL.Base_ObjectDAL dal = new DAL.Base_ObjectDAL();
            model = dal.GetDataForType<Model.Base_ObjectModel>(id, "Base_Object");
            if (model.create_user != GetUser().id)
            {
                return Error("未找到相关数据，请刷新后再试");
            }
            model.name = name;
            model.explain = explain;
            model.sort = sort;
            model.Modify();
            model.last_user = GetUser().id;
            int res = dal.Update<Model.Base_ObjectModel>(model, "Base_Object");
            if (res > 0)
            {
                return SuccessMsg("修改成功");
            }
            else
            {
                return Error("系统异常，请稍后再试");
            }

        }

        [HttpPost]
        public ActionResult DeleteType(string id)
        {
            DAL.Base_TypeDAL dal = new DAL.Base_TypeDAL();
            int res = dal.Delete(id, "Base_Type");
            if (res > 0)
            {
                return SuccessMsg("删除成功");
            }
            else {
                return Error("网络繁忙，请稍后再试");
            }
        }

        [HttpPost]
        public ActionResult DeleteObject(string id)
        {
            DAL.Base_ObjectDAL dal = new DAL.Base_ObjectDAL();
            int res = dal.Delete(id, "Base_Object");
            if (res > 0)
            {
                return SuccessMsg("删除成功");
            }
            else
            {
                return Error("网络繁忙，请稍后再试");
            }
        }
    }
}
