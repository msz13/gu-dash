import { MutationResponse } from "./util-graphql"

describe('graphql mutation respons', ()=>{

    it('should return succes method', ()=>{
        const response = MutationResponse.success('200', 'ok', [['adress', {street: "Kurczaki"} ]])

        console.log(JSON.stringify(response, null, 5))

        expect(response.succes).toBeTruthy()
        expect(response.code).toMatch('200')
        expect(response.message).toMatch('ok')
        expect(response).toHaveProperty('adress', {street: "Kurczaki"} )

    })

   
})