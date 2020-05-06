import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { CompetencesModule } from './competences/competences.module';
import { GraphQLModule } from '@nestjs/graphql';
import { UserAccauntModule } from './user-accaunt/user-accaunt.module';

@Module({
  imports: [
    GraphQLModule.forRoot({
      typePaths: ['./**/*.graphql']
    }),
    CompetencesModule,
    UserAccauntModule
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
