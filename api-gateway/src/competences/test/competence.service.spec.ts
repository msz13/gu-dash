/**
 * @jest-environment node
 */


import { Test, TestingModule } from '@nestjs/testing';
import { CompetenceService } from '../competence.service';
import {Pact, Interaction} from '@pact-foundation/pact'
import { uuid, like } from '@pact-foundation/pact/dsl/matchers';
import { CreateCompetenceDTO, Competence, CreateCompetenceResponse } from 'src/graphql';
import * as path from 'path';
import { CompetencesModule } from '../competences.module';
import {pactWith} from 'jest-pact';


pactWith({ 
  consumer: 'CompetencesService', 
  provider: 'Api-Gateway',
  port: 5000,
}, (provider: Pact) => {
  
    let service: CompetenceService;
    let baseUrl = 'http://localhost:5000'

 
  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      imports: [CompetencesModule],      
    }).compile();

    service = module.get<CompetenceService>(CompetenceService);
  });

  

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  'contexts - create and find, anauthorised, bad input, error result,'
  describe('should create competence', ()=>{

    
    const COMPETENCE_INPUT: CreateCompetenceDTO = {
      name: 'Uprzejmość',
      isRequired: true,
      description: "Na codzień"
    }

    const EXPECTED_COMPETENCE = {
      Id: '41209094-fb28-499e-a976-97dfa03134ad', 
      owner: '123',
      name: COMPETENCE_INPUT.name,
      description: COMPETENCE_INPUT.description,
      isRequired: false,
      numberOfActiveGoals: 0,
      numberOfDoneGoals: 0,
      numberOfHoldedGoals: 0
    }
    
    const interactionCreate = new Interaction()
      .uponReceiving('request for creating competence')
      .withRequest({
        method: 'POST',
        path: '/competence',
        headers: {
          'X-Authenticated-Accaunt-Id': '123'
        },
        body: COMPETENCE_INPUT
      })
      .willRespondWith({
        status: 201,
        headers: {
          'Location': 'http://localhost:5000/competence/41209094-fb28-499e-a976-97dfa03134ad'
        },
      })


      const interactionGet = new Interaction()
      .uponReceiving('request for creating competence')
      .withRequest({
        method: 'GET',
        path: '/competence/41209094-fb28-499e-a976-97dfa03134ad',
        headers: {          
          'X-Authenticated-Accaunt-Id': '123'
        },
      })
      .willRespondWith({
        status: 200,
        headers: {
                  
        },
        body: like({...EXPECTED_COMPETENCE, Id: uuid(EXPECTED_COMPETENCE.Id)})
      })

      beforeEach(()=> {
       return provider.addInteraction(interactionCreate)
      })

      beforeEach(()=> {
         return provider.addInteraction(interactionGet)
        })

      it('should create competence', done =>{
        service.createAndGet(COMPETENCE_INPUT, '123').subscribe(
          (response: CreateCompetenceResponse )=> {
            expect(response.success).toBe(true)
            expect(response.competence).toMatchObject(EXPECTED_COMPETENCE)
          done()
        })           
                 
     
      })

  })

  describe('non unique name should reuturn failure response', ()=>{

    const COMPETENCE_INPUT: CreateCompetenceDTO = {
      name: 'uprzejmość',
      isRequired: true,
      description: ""
    }

    const interactionCreate = new Interaction()
      .uponReceiving('unauthorised request')
      .given('competence with name uprzejmosc')
      .withRequest({
        method: 'POST',
        path: '/competence',
        headers: {
          'X-Authenticated-Accaunt-Id': '123'
        },
        body: COMPETENCE_INPUT
      })
      .willRespondWith({
        status: 400,
        body: {error: {
          code: 'COMPETENCE_NON_UNIQUE_NAME',
          message: 'Competence with name "Uprzejmość"'
        }
        }
      })

     beforeEach(()=> {
        return provider.addInteraction(interactionCreate)
       })

       it('should create error response', done =>{
         service.createAndGet(COMPETENCE_INPUT, '123')
          .subscribe((response: CreateCompetenceResponse) => {
            expect(response.success).toBe(false)
            expect(response.error.code).toBe('COMPETENCE_NON_UNIQUE_NAME')
            expect(response.error.message).toBeDefined()
            done()
          })

       })  

  })

  describe('no auth header shoud return error', () => 
  {
    const COMPETENCE_INPUT: CreateCompetenceDTO = {
      name: 'Uprzejmość',
      isRequired: true,
      description: ""
    }

    const interactionCreate = new Interaction()
      .uponReceiving('request for creating competence')
      .given('competence with name uprzejmosc')
      .withRequest({
        method: 'POST',
        path: '/competence',
        headers: {         
        },
        body: COMPETENCE_INPUT
      })
      .willRespondWith({
        status: 500,                 
        
      })

      beforeEach(()=> {
        return provider.addInteraction(interactionCreate)
       })

      it('should send 500 status code', done => {
        service.createAndGet(COMPETENCE_INPUT, '123').subscribe({
          next(response) { expect(response).toBeUndefined()},
          error(error) { expect(error).toBeDefined()
            done()}
          
        })

      })


  })

  describe('bad input shoud return server error', () => 
  {
    const COMPETENCE_INPUT: CreateCompetenceDTO = {
      name: '',
      isRequired: true,
      description: ""
    }

    const interactionCreate = new Interaction()
      .uponReceiving('request for creating competence with bad input')
      .withRequest({
        method: 'POST',
        path: '/competence',
        headers: {         
          "X-Authenticated-Accaunt-Id": "123"
        },
        body: COMPETENCE_INPUT
      })
      .willRespondWith({
        status: 500,                
        
      })

      beforeEach(()=> {
        return provider.addInteraction(interactionCreate)
       })

      it('should send 500 status code', done => {
        service.createAndGet(COMPETENCE_INPUT, '123').subscribe({
          next(response) { expect(response).toBeUndefined()},
          error(error) { expect(error).toBeDefined()
            done()}
          
        })

      })


  })
  
});
