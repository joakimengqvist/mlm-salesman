import { useState, useEffect } from 'react'
import axios from 'axios'
import SimulationComponent from './components/simulation/Simulation'
import './App.css'
import { ApiResponse, Simulation } from './interfaces'
import BottomPanel from './components/bottomPanel/bottomPanel'

/*

for different scenarios change the query params in the request:

rows: number of rows (X-axis)
columns: number of columns (Y-axis)
quantity: number of simulations

*/

function App() {
    const [simulations, setSimulations] = useState<Simulation[]>([])
    const [averageHours, setAverageHoursHours] = useState<number>(0)
    const [currentStep, setCurrentStep] = useState<number>(0)
    const [hasFetched, setHasFetched] = useState(false)

    useEffect(() => {
        const fetchData = async () => {
            await axios
                .get<ApiResponse>(
                    'http://localhost:5175/api/recruitment/simulate?rows=10&columns=10&quantity=200'
                )
                .then((response) => {
                    setSimulations(response.data.simulations || [])
                    setAverageHoursHours(response.data.averageHours || 0)
                    setHasFetched(true)
                })
                .catch((error) => {
                    setHasFetched(true)
                    setAverageHoursHours(0)
                    setSimulations([])
                    console.error('Error fetching data:', error)
                })
        }
        if (!hasFetched) {
            fetchData()
        }
    }, [])

    const nextStep = () => {
        setCurrentStep((prev) => prev + 1)
    }

    const prevStep = () => {
        if (currentStep > 0) {
            setCurrentStep((prev) => prev - 1)
        }
    }

    const playSteps = () => {
        setInterval(() => {
            setCurrentStep((prev) => prev + 1)
        }, 1000)
    }

    if (!hasFetched) {
        return <div>Loading...</div>
    }

    if (!simulations.length) {
        return <div>No data</div>
    }

    return (
        <div className="app">
            <div className="simulation-container">
                {simulations.map((simulation, index) => (
                    <SimulationComponent
                        key={`simulation-${index}`}
                        index={index}
                        history={simulation.history}
                        currentStep={currentStep}
                    />
                ))}
            </div>

            <BottomPanel
                averageHours={averageHours}
                isPrevDisabled={currentStep === 0}
                onPrevStep={prevStep}
                onNextStep={nextStep}
                onPlaySteps={playSteps}
            />
        </div>
    )
}

export default App
