import { Module, HttpModule } from '@nestjs/common';
import { CompetenceResolver } from './competence.resolver';
import { CompetenceService } from './competence.service';
import { GraphQLModule } from '@nestjs/graphql';

@Module({
  imports: [
    HttpModule,
    

  ],
  providers: [CompetenceResolver, CompetenceService]
})
export class CompetencesModule {}
