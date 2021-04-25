using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuccaSA.Cleemy.Low;
using LuccaSA.Cleemy.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RecruitmentTest.Controllers
{
    #region CleemyController

    [Route("api/[controller]")]
    [ApiController]
    public class CleemyController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly ModelValidation _validation;

        public CleemyController(ILogger<CleemyController> logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _validation = new ModelValidation(_repository);
        }

        #region GET

        // GET: api/Cleemy/depenses
        [HttpGet("depenses")]
        public async Task<ActionResult<IEnumerable<DepenseDTO>>> GetDepenses()
        {
            try
            {
                //On récupère toutes les dépenses sans distinction
                IEnumerable<DepenseDB> v_listDepenseDB = await _repository.Depense.GetAllDepenseDB();

                //On associe les propriétés de notre source (DepenseDB) vers l'objet de destination (DepenseDTO)
                IEnumerable<DepenseDTO> v_depensesDTO = _mapper.Map<IEnumerable<DepenseDTO>>(v_listDepenseDB);

                _logger.LogInformation($"Toutes les dépenses stockées en base ont été retournés.");

                return Ok(v_depensesDTO);
            }
            catch (Exception v_ex)
            {
                _logger.LogError($"Erreur depuis  GetDepenses : {v_ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Cleemy/depenses/1
        [HttpGet("depenses/{id}")]
        public async Task<ActionResult<IEnumerable<DepenseDTO>>> GetDepenseById(long id)
        {
            try
            {
                //On récupère la dépense par id
                DepenseDB v_depenseDB = await _repository.Depense.GetDepenseDBById(id);

                if (v_depenseDB == null)
                {
                    _logger.LogError($"La dépense avec l'id: {id}, n'a pas été trouvée.");
                    return NotFound();
                }

                //On associe les propriétés de notre source (DepenseDB) vers l'objet de destination (DepenseDTO)
                DepenseDTO v_depenseDTO = _mapper.Map<DepenseDTO>(v_depenseDB);

                _logger.LogInformation($"Dépense retournée.");

                return Ok(v_depenseDTO);
            }
            catch (Exception v_ex)
            {
                _logger.LogError($"Erreur depuis GetDepenseById : {v_ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Cleemy/utilisateurs/Stark,Anthony/depenses?orderBy=Date
        [HttpGet("utilisateurs/{nom},{prenom}/depenses")]
        public async Task<ActionResult<DepenseDTO>> GetListDepensesByUtilisateur([FromQuery] DepenseGET p_depenseGET)
        {
            try
            {
                //On récupère toutes les dépenses d'un utilisateur à partir de son nom et prénom
                IEnumerable<DepenseDB> v_listDepenseDB = await _repository.Depense.GetAllDepenseDBByUtilisateur(p_depenseGET.nom, p_depenseGET.prenom);

                // On trie ensuite ses dépenses
                v_listDepenseDB = _repository.Depense.SortBy(v_listDepenseDB.AsQueryable(), p_depenseGET.orderBy);
                if (v_listDepenseDB.Count() == 0)
                {
                    _logger.LogError($"Aucun utilisateur n'a été trouvé.");
                    return NotFound();
                }
                else
                {
                    //On associe les propriétés de notre source (DepenseDB) vers l'objet de destination (DepenseDTO)
                    IEnumerable<DepenseDTO> v_depenseDTOResult = _mapper.Map<IEnumerable<DepenseDTO>>(v_listDepenseDB);

                    _logger.LogInformation($"Dépenses retournées pour un utilisateur");
                    return Ok(v_depenseDTOResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur depuis GetListDepensesByUtilisateur: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

        #region POST

        // POST: api/Cleemy/depenses
        [HttpPost("depenses")]
        public async Task<ActionResult<DepenseDTO>> PostDepense([FromBody] DepensePOST p_depensePOST)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Erreur dans le json.");
                    return BadRequest(ModelState);
                }

                //On associe les propriétés de notre source (DepensePOST) vers l'objet de destination (DepenseDB)
                DepenseDB v_depenseDB = _mapper.Map<DepenseDB>(p_depensePOST);

                // On vérifie que l'objet créé est bien conforme
                string v_messageError = _validation.DepenseDBIsValid(v_depenseDB);

                if (string.IsNullOrWhiteSpace(v_messageError))
                {
                    // Si la dépense est conforme on l'enregistre en base
                    _repository.Depense.AddDepenseDB(v_depenseDB);
                    await _repository.SaveAsync();
                }
                else
                {
                    _logger.LogError(v_messageError);
                    return BadRequest(v_messageError);
                }

                //On associe les propriétés de notre source (DepenseDB) vers l'objet de destination (DepenseDTO)
                var v_depenseDTO = _mapper.Map<DepenseDTO>(v_depenseDB);

                _logger.LogInformation($"Dépense créé avec succés.");

                return Ok(v_depenseDTO);
            }
            catch (Exception v_ex)
            {
                _logger.LogError($"Erreur depuis PostDepense : {v_ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        #endregion

    }

    #endregion
}
