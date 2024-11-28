import React from 'react'
import './bottomPanel.css'

interface BottomPanelProps {
    averageHours: number
    isPrevDisabled: boolean
    onPrevStep: () => void
    onNextStep: () => void
    onPlaySteps: () => void
}

const BottomPanel: React.FC<BottomPanelProps> = ({
    averageHours,
    isPrevDisabled,
    onPrevStep,
    onNextStep,
    onPlaySteps,
}) => (
    <div className="bottom-panel">
        <p className="bottom-panel-result">
            Average hours for all simulations: <strong>{averageHours}</strong>
        </p>
        <div className="simulation-controls">
            <button onClick={onPrevStep} disabled={isPrevDisabled}>
                Previous Step
            </button>
            <button onClick={onNextStep}>Next Step</button>
            <button onClick={onPlaySteps}>Play</button>
        </div>
    </div>
)

export default BottomPanel
