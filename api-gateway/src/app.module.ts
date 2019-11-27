import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { CompetencesModule } from './competences/competences.module';
import { GraphQLModule } from '@nestjs/graphql';

@Module({
  imports: [
    GraphQLModule.forRoot({
      typePaths: ['./**/*.graphql']
    }),
    CompetencesModule
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
