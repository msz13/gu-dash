import { Module, HttpModule } from '@nestjs/common';
import { CompetenceResolver } from './competence.resolver';
import { CompetenceService } from './competence.service';
import { GraphQLModule } from '@nestjs/graphql';
import { timeout } from 'rxjs/operators';

@Module({
  imports: [
    HttpModule.register({
      baseURL: 'http://localhost:5000', //TODO use config service
      timeout: 1000,
      validateStatus: (status) => {
        return status >= 200 && status < 300 || status == 400; // default
      },

    })
  ],
  providers: [CompetenceResolver, CompetenceService]
})
export class CompetencesModule {}
