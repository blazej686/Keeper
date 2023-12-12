

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly Auth0Provider _auth0Provider;
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly VaultsService _vaultsService;

        public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService, VaultsService vaultsService)
        {
            _auth0Provider = auth0Provider;
            _vaultKeepsService = vaultKeepsService;
            _vaultsService = vaultsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepsData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Vault vaultData = _vaultsService.GetVaultById(vaultKeepsData.VaultId, userInfo.Id);
                vaultKeepsData.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepsData, userInfo.Id, vaultData);
                return Ok(vaultKeep);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{vaultKeepId}")]
        [Authorize]
        public async Task<ActionResult<string>> DestroyVaultKeep(int vaultKeepId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultKeepsService.DestroyVaultKeep(vaultKeepId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
