import React from 'react'
import './simulation.css'
import { HistoryStep } from '../../interfaces'
import Grid from '../grid/Grid'

interface SimulationProps {
    index: number
    history: HistoryStep[]
    currentStep: number
}

const Simulation: React.FC<SimulationProps> = ({
    index,
    history,
    currentStep,
}) => {
    const step = currentStep < history.length ? currentStep : history.length - 1
    const currentHistoryStep = history[step]

    return (
        <div className="simulation">
            <h3>Simulation {index + 1}</h3>
            <p>
                Hour {step + 1} of {history.length}
            </p>
            <Grid
                houses={currentHistoryStep.houses}
                salesmen={currentHistoryStep.salesmen}
            />
        </div>
    )
}

export default Simulation
