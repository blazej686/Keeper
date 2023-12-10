using Microsoft.AspNetCore.Http.HttpResults;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly Auth0Provider _auth0Provider;
        private readonly VaultsService _vaultsService;
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsController(Auth0Provider auth0Provider, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
        {
            _auth0Provider = auth0Provider;
            _vaultsService = vaultsService;
            _vaultKeepsService = vaultKeepsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vaultsService.CreateVault(vaultData);
                return Ok(vault);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}")]
        public async Task<ActionResult<List<Vault>>> GetVaultById(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Keep> keeps = _vaultKeepsService.GetKeepsByVaultId(vaultId, userInfo.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{vaultId}")]
        [Authorize]
        public async Task<ActionResult<Vault>> UpdatedVault(int vaultId, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.UpdateVault(vaultId, userInfo.Id, vaultData);
                return vault;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{vaultId}")]
        [Authorize]
        public async Task<ActionResult<string>> DestroyVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultsService.DestroyVault(vaultId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}