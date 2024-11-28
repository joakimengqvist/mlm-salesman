export interface House {
    x: number
    y: number
    hasUnemployedPerson: boolean
}

export interface Salesman {
    x: number
    y: number
}

export interface HistoryStep {
    houses: House[]
    salesmen: Salesman[]
}

export interface Simulation {
    history: HistoryStep[]
    hoursPassed: number
}

export interface ApiResponse {
    simulations: Simulation[]
    averageHours: number
}
