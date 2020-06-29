using IRepository;
using IServices;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ActionServices : BaseServices<Model.Entitys.Action>, IActionServices
    {
        private readonly IActionRepository _actionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleActionRepository _roleActionRepository;

        public ActionServices(IActionRepository actionRepository, IRoleRepository roleRepository, IRoleActionRepository roleActionRepository)
        {
            _actionRepository = actionRepository;
            _roleRepository = roleRepository;
            _roleActionRepository = roleActionRepository;
            base.CurrentRepository = _actionRepository;
        }

        public async Task<PagedList<Model.Entitys.Action>> GetActionPaged(ActionParams actionParams)
        {
            IQueryable<Model.Entitys.Action> actions = _actionRepository.GetEntitys();
            //判断为几级权限
            if (actionParams.Level > 0)
            {
                actions = actions.Where(a => a.ActionTypeId == actionParams.Level);
            }
            //通过名称模糊查询
            if (!string.IsNullOrWhiteSpace(actionParams.Name))
            {
                actions = actions.Where(a => a.Name.Contains(actionParams.Name));
            }
            return await PagedList<Model.Entitys.Action>.CreatePagedList(actions, actionParams.PageSize, actionParams.PageNum);
        }

        public async Task<bool> SetRoleAction(int roleId, IEnumerable<int> actionId)
        {
            Role role = await _roleRepository.GetEntityByIdAsync(roleId);
            IEnumerable<RoleAction> roleActions = role.RoleAction;
            foreach (var item in roleActions)
            {
                _roleActionRepository.DeleteEntity(item);
            }
            foreach (var item in actionId)
            {
                RoleAction roleAction = new RoleAction() { RoleId = roleId, ActionId = item };
                _roleActionRepository.AddEntityAsync(roleAction);
            }
            return await _roleActionRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAction(int id)
        {
            var entity = await _actionRepository.GetEntityByIdAsync(id);
            var childrenAction = await _actionRepository.GetEntitys().Where(a => a.Pid == entity.Id).ToListAsync();
            //对子节点的删除操作
            foreach (var actionItem in childrenAction)
            {
                _actionRepository.DeleteEntity(actionItem);
                var roleAction = actionItem.RoleAction.ToList();
                foreach (var roleActionItem in roleAction)
                {
                    _roleActionRepository.DeleteEntity(roleActionItem);
                }
            }
            //对父节点的操作
            _actionRepository.DeleteEntity(entity);
            foreach (var item in entity.RoleAction.ToList())
            {
                _roleActionRepository.DeleteEntity(item);
            }
            return await _actionRepository.SaveChangesAsync();
        }
    }
}