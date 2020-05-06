import { Injectable, HttpService } from '@nestjs/common';
import { Observable, of, throwError, empty } from 'rxjs';
import { AxiosResponse } from 'axios';
import { map, switchMapTo, switchMap, tap, delay, catchError } from 'rxjs/operators';
import { CreateCompetenceDTO, CreateCompetenceResponse, MutationError } from 'src/graphql';
import { MutationResponse } from '../common/util-graphql';
import { Mutation } from '@nestjs/graphql';
import { response } from 'express';


@Injectable()
export class CompetenceService {
    constructor(private readonly httpService: HttpService) { }

    findAll() {
        return this.httpService.get('http://localhost:5000/api/competences').pipe(map(res => res.data))
    }

    async findCreated(uri: string) {

        return await this.httpService.get(uri).pipe(map(resp => resp.data)).toPromise()

    }

    createAndGet(competenceDTO: CreateCompetenceDTO, accauntId: string): Observable<any> {

        return this.httpService.post('/competence', competenceDTO, this.setAuthHeader(accauntId))
            .pipe(
                /* switchMap(response => response.status == 201 ?
 
                     this.httpService.get(response.headers.location, this.setAuthHeader(accauntId))
                         .pipe(map(res => MutationResponse.succes({ competence: res.data })))
 
                     : of(MutationResponse.failure(response.data.error as MutationError))
                 ),
 
                  */

                 switchMap(response => {if(response.status == 201) {
                     return this.httpService.get(response.headers.location, this.setAuthHeader(accauntId))
                     .pipe(map(res => MutationResponse.succes({ competence: res.data })))

                 } else if (response.status == 400 ) {
                     return of(MutationResponse.failure(response.data.error as MutationError))
                 } else {
                     return empty()
                 }
                })
                )     
                
    }
                   
        
    

    private setAuthHeader(accauntId: string) {
        return {
            headers: { "X-Authenticated-Accaunt-Id": accauntId }
        }

    }
}
