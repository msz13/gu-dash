import { Injectable, HttpService } from '@nestjs/common';
import { Observable, of } from 'rxjs';
import { AxiosResponse } from 'axios';
import { map, switchMapTo, switchMap, tap, delay, catchError } from 'rxjs/operators';
import { CreateCompetenceDTO } from 'src/graphql';


@Injectable()
export class CompetenceService {
    constructor(private readonly httpService: HttpService) {}

    findAll() {
        return this.httpService.get('http://localhost:5000/api/competence').pipe(map(res=>res.data))
    }

    async findCreated(uri: string) {

        return await this.httpService.get(uri).pipe(map(resp=>resp.data)).toPromise()

    }

    async createAndFind(competenceDTO: CreateCompetenceDTO) {

        return await this.httpService.post('http://localhost:5000/api/competence', competenceDTO )
            .pipe(
                map(resp=>resp.headers.location),
                delay(5),
                switchMap(uri => this.httpService.get(uri).pipe(map(res=>res.data))),
                catchError(error=>{
                    console.log(error)
                    return of()
                })
            )
            .toPromise()            
           
       }
    
}
