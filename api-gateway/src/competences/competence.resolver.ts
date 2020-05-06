import { Resolver, Query, ResolveProperty, Parent, Mutation, Args, Context } from '@nestjs/graphql';
import { CompetenceService } from './competence.service';
import { Competence, CreateCompetenceDTO, CreateCompetenceResponse } from '../graphql';
import { MutationResponse } from '../common/util-graphql';
import { map } from 'rxjs/operators';

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
     createCompetence(@Args('createCompetenceInput') competenceDTO: CreateCompetenceDTO, @Context() context: any) {
       
        return this.competenceService.createAndGet(competenceDTO, context.userId)           
                    
    }

   
}
