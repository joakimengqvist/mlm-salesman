import React from 'react'
import { House, Salesman } from '../../interfaces'
import './grid.css'

interface GridProps {
    houses: House[]
    salesmen: Salesman[]
}

const Grid: React.FC<GridProps> = ({ houses, salesmen }) => {
    const maxX = Math.max(...houses.map((house) => house.x), 0)
    const maxY = Math.max(...houses.map((house) => house.y), 0)

    return (
        <div className="grid">
            {Array.from({ length: maxY + 1 }, (_, y) => (
                <div key={'y' + y} className="row">
                    {Array.from({ length: maxX + 1 }, (_, x) => {
                        const house = houses.find(
                            (house) => house.x === x && house.y === y
                        )
                        const hasSalesman = salesmen.some(
                            (salesman) => salesman.x === x && salesman.y === y
                        )
                        const hasUnemployedPerson = house?.hasUnemployedPerson
                            ? 'unemployed'
                            : ''

                        return (
                            <div
                                key={`${x}-${y}`}
                                className={`house ${hasUnemployedPerson} ${hasSalesman ? 'salesman' : ''}`}
                            />
                        )
                    })}
                </div>
            ))}
        </div>
    )
}

export default Grid
