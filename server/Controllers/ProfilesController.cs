

using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {

        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth0Provider;


        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth0Provider)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _auth0Provider = auth0Provider;
        }

        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfileById(string profileId)
        {
            try
            {
                Profile profile = _profilesService.GetProfileById(profileId);
                return Ok(profile);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public async Task<ActionResult<List<Keep>>> GetKeepsByProfileId(string profileId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId, userInfo?.Id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo?.Id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}