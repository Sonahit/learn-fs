namespace Application.Types


module Stdin =
    type InputType =
        | String = 's'
        | Int = 'i'

    type Input = { Name: string; Type: InputType }
