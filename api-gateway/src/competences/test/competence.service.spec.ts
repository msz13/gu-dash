import { Test, TestingModule } from '@nestjs/testing';
import { CompetenceService } from '../competence.service';
import { HttpModule } from '@nestjs/common';
import {map} from 'rxjs/operators'

describe('CompetenceService', () => {
  let service: CompetenceService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      imports: [HttpModule],
      providers: [CompetenceService],
    }).compile();

    service = module.get<CompetenceService>(CompetenceService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  it('shuld get competence', async ()=>{
    console.time('get competences')
    const result =  await service.findAll()
    console.timeEnd('get competences')
    console.log(JSON.stringify(result, null, 5))
  })
});
