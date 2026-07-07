using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Data;
using pos_api_app.DTOs.RolesDTO;
using pos_api_app.Models.Entities;

namespace pos_api_app.Services;

public class RoleService
{
    private readonly IRoleRepository _roleService;
    private readonly PosDbContext _context;

    public RoleService(IRoleRepository roleService, PosDbContext context)
    {
        _roleService = roleService;
        _context = context;
    }

    public async Task<IEnumerable<RoleDTO>?> GetAll()
    {
        var list = await _roleService.GetAll();
        if (list == null || !list.Any())
        {
            return null;
        }

        var listDto = list.Select(role => (RoleDTO)role);
        return listDto;
    }

    public async Task<int> Create(NewRoleDTO role)
    {
        var roleEntity = (Role)role;
        var create = await _roleService.Create(roleEntity);
        if (create is null) return 0;
        return 1;
    }

    public async Task<int> Delete(int id)
    {
        var roleEntity = await _roleService.GetById(id);
        if (roleEntity == null) return -1;

        var delete = await _roleService.Delete(roleEntity);
        if (delete == false) return 0;
        return 1;
    }
}
