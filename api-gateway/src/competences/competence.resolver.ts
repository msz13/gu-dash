import { Resolver, Query, ResolveProperty, Parent, Mutation, Args } from '@nestjs/graphql';
import { CompetenceService } from './competence.service';
import { Competence, CreateCompetenceDTO, CreateCompetenceResponse } from '../graphql';

@Resolver('Competence')
export class CompetenceResolver {

    constructor(private readonly competenceService: CompetenceService) { }


    @Query("competences")
    getCompetences(){
        
        const result = this.competenceService.findAll() 
      //  console.log(JSON.stringify(result[1].competenceId))
        return result
    }

    @Mutation()
    async createCompetence(@Args('createCompetenceInput') competenceDTO: CreateCompetenceDTO){
       
        const result = await this.competenceService.createAndFind(competenceDTO)

        const resp = new CreateCompetenceResponse()

        resp.success = true
        resp.competence = result        

        return resp

    }

   
}
