using Microsoft.AspNetCore.Mvc;
using RestRecipeApp.Core.RequestDto;
using RestRecipeApp.Persistence.Models;
using RestRecipeApp.Persistence.Repositories;
using RestRecipeApp.Validation;

namespace RestRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepController : ControllerBase
    {
        private readonly IRecipeStepRepository _recipeStepRepository;

        public RecipeStepController(IRecipeStepRepository recipeStepRepository)
        {
            _recipeStepRepository = recipeStepRepository;
        }

        // GET: api/RecipeStep
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeStep>>> GetRecipeSteps()
        {
            var result = await _recipeStepRepository.GetRecipeSteps();
            return result.Right<ActionResult>(response =>
            {
                return Ok(response.Map(recipeStep => recipeStep.MapGetRecipeStepDto()));
            }).Left(BadRequest);
        }

        // GET: api/RecipeStep/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeStep>> GetRecipeStep(int id)
        {
            var recipeStepOrError = await _recipeStepRepository.GetRecipeStepById(id);
            return recipeStepOrError
                .Right<ActionResult>(foundRecipeStep => Ok(foundRecipeStep.MapGetRecipeStepDto()))
                .Left(_ => NotFound($"Could not find recipe step with id: {id}"));
        }

        // PUT: api/RecipeStep/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRecipeStep(int id, UpdatedRecipeStepDto updateRecipeStep)
        {
            if (id != updateRecipeStep.Id)
            {
                return BadRequest("Ids are not the same");
            }

            var updatedRecipeStep = await _recipeStepRepository.UpdateRecipeStep(updateRecipeStep);
            return updatedRecipeStep
                .Right<ActionResult>(recipeStep =>
                    CreatedAtAction("GetRecipeStep", new { id = recipeStep.RecipeStepId }, recipeStep))
                .Left(error => BadRequest(error.Message));
        }

        // POST: api/RecipeStep
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeStep>> PostRecipeStep(CreateRecipeStepDto createRecipeStepDto)
        {
            var validator = new CreateRecipeStepDtoValidator();
            var validatedResult = validator.Validate(createRecipeStepDto);
            if (!validatedResult.IsValid)
            {
                return BadRequest($"Recipe step is not valid - {string.Join(", ", validatedResult.Errors)}");
            }
            var createdRecipeStepOrError = await _recipeStepRepository.CreateRecipeStep(createRecipeStepDto);

            return createdRecipeStepOrError
                .Right<ActionResult>(createdRecipeStep =>
                    CreatedAtAction("GetRecipeStep", new { id = createdRecipeStep.RecipeStepId }, createdRecipeStep))
                .Left(error => BadRequest(error.Message));
        }

        // DELETE: api/RecipeStep/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeStep(int id)
        {
            var isFoundAndRemove = await _recipeStepRepository.RemoveRecipeStep(id);
            if (!isFoundAndRemove)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}