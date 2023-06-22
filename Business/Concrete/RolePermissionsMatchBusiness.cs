using Business.Abstract;
using Core.Entities.Dtos.Models;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RolePermissionsMatchBusiness : IRolePermissionsMatchBusiness
    {
        private IRolePermissionsMatchDal rolePermissionsMatchDal;

        public RolePermissionsMatchBusiness(IRolePermissionsMatchDal rolePermissionsMatchDal)
        {
            this.rolePermissionsMatchDal = rolePermissionsMatchDal;
        }

        public IResult Add(RolePermissionsMatch entity)
        {
            return rolePermissionsMatchDal.Add(entity);
        }

        public IResult Delete(RolePermissionsMatch entity)
        {
            return rolePermissionsMatchDal.Delete(entity);
        }

        public IDataResult<RolePermissionsMatch> GetById(long id)
        {
            return rolePermissionsMatchDal.Get(x => x.Id == id);
        }

        public IDataResult<List<RolePermissionsMatch>> GetList()
        {
            return rolePermissionsMatchDal.GetList();
        }

        public IDataResult<List<RolePermissionsMatch>> GetList(Expression<Func<RolePermissionsMatch, bool>> filter)
        {
            return rolePermissionsMatchDal.GetList(filter);
        }

        public IDataResult<List<AuthorizationModel>> GetMenuList(long UserTypeId, long langId)
        {
            var authModel = new List<AuthorizationModel>();

            var rolePermissionsMatchList = rolePermissionsMatchDal.GetList(x => x.UserTypeId == UserTypeId)?.Data?.Select(x => x.RolePermissionsId);

            using (var context = new AvukatPortalContext())
            {
                var responseFromDb = from rm in context.RolePermissionsMatch
                           join rl in context.RolePermissions
                           on rm.RolePermissionsId equals rl.Id
                           join d in context.Description
                           on rl.Id equals d.RolePermissionsId
                           join l in context.Language
                           on d.LanguageId equals l.Id
                           where rl.StatusId == 1 && d.StatusId == 1 && l.StatusId == 1 && rolePermissionsMatchList.Contains(rm.RolePermissionsId)
                           select new MenuList
                           {
                               Id = rl.Id,
                               Name = rl.Name ?? string.Empty,
                               Value = d.Value ?? string.Empty,
                               MenuIconClass = rl.MenuIconClass ?? string.Empty,
                               ControllerName = rl.ControllerName ?? string.Empty,
                               ActionName = rl.ActionName ?? string.Empty,
                               ParentRoleId = rl.ParentRoleId.Value,
                               RolePermissionsTypeId = rl.RolePermissionsTypeId.Value,
                               RolePriority = rl.RolePriority.Value,
                           };

                List<MenuList> menuTypeList = GetMenuRoleParent(responseFromDb, (long)EnumRolePermissionsType.All);

                foreach (var menuHead in menuTypeList)
                {
                    var menuHeader = new AuthorizationModel();
                    var menuHeaderItems = new List<MenuItems>();

                    menuHeader.MenuName = menuHead.Value ?? string.Empty;
                    var elementsOfMenuHeader = GetMenuRoleParent(responseFromDb, menuHead.Id);

                    foreach (var elementOfMenuHeader in elementsOfMenuHeader)
                    {
                        var menuItem = new MenuItems();
                        
                        menuItem.ControllerName = elementOfMenuHeader.ControllerName ?? string.Empty;
                        menuItem.ActionName = elementOfMenuHeader.ActionName ?? string.Empty;
                        menuItem.MenuName = elementOfMenuHeader.Value ?? string.Empty;
                        menuItem.Priorty = elementOfMenuHeader.RolePriority;
                        menuItem.MenuIconClass = elementOfMenuHeader.MenuIconClass ?? string.Empty;
                        menuItem.SubMenus = new List<SubMenuItems>();

                        menuHeaderItems.Add(menuItem);

                        var elementsOfMenu = GetMenuRoleParent(responseFromDb, elementOfMenuHeader.Id);

                        foreach (var elementOfMenu in elementsOfMenu)
                        {
                            var subMenuItem = new SubMenuItems
                            {
                                ActionName = elementOfMenu.ActionName ?? string.Empty,
                                ControllerName= elementOfMenu.ControllerName ?? string.Empty,
                                MenuIconClass = elementOfMenu.MenuIconClass ?? string.Empty,
                                MenuName = elementOfMenu.Value ?? string.Empty,
                                Priorty = elementOfMenu.RolePriority,
                                Actions = new List<string>()
                            };

                            menuItem.SubMenus.Add(subMenuItem);

                            var asd2 = GetMenuRoleParent(responseFromDb, elementOfMenu.Id);

                            foreach (var item in asd2)
                            {
                                subMenuItem.Actions.Add(item.Name ?? string.Empty);
                            }
                        }
                    }
                    menuHeader.UserPermissions = menuHeaderItems;
                    authModel.Add(menuHeader);
                }
            }

            return new SuccessDataResult<List<AuthorizationModel>>(authModel);
        }

        private static List<MenuList> GetMenuRoleParent(IQueryable<MenuList> rest, long Id)
        {
            return rest.Where(x => x.ParentRoleId == Id).ToList();
        }

        public IResult Update(RolePermissionsMatch entity)
        {
            return rolePermissionsMatchDal.Update(entity);
        }
    }
}
