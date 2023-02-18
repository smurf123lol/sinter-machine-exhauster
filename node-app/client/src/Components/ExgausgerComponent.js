import React from "react";
import  "../css/exgauster.css"
import svg_exg from '../img/exgauster.svg'

class ExgausterComponent extends React.Component{
    constructor(props){
        super(props);
        this.props = props;
        this.state = {
            open:false
        }
    }
    OpenExgausterPage(){

    }
    UpdateOpenState(){
        this.setState({
            open:!this.state.open
        });
    }
    render(){
        return <div className="exgauster-wrapper">
            <div><div className="exgauster-head">
                <div className="ex-blink"></div>
                {this.props.data.ex_name}
                <button className="exgauster-button" onClick={this.OpenExgausterPage.bind(this)}>></button>
            </div></div>
            <div>
            <div className="exgauster-container">
                <div className='ex-rotor-data'>
                    Ротор №###
                    <div className='ex-rotor-date'>00-00-0000</div>
                    <a className='ex-rotor-change' href="#">Изменить</a>
                    <hr></hr>
                    <h5>Последняя замена ротора</h5>
                    <div className='rotor-exp-time'>## сут</div>
                    <div className='rotor-forecast'>
                        <div className="rotor-forecast-label">Прогноз</div>
                        ## сут
                    </div>
                </div>
                <img className='exgauster-image' src={svg_exg} alt="Эксгаустер"></img>
                <div>
                    <h4>Предупреждение</h4>
                    <div className='alert-container'>
                        <li className="menu-item">
                            Подшипник #1
                        </li>
                    </div>
                    <div className="menu-arrow" onClick={this.UpdateOpenState.bind(this)}>{this.state.open ? '˅':'>'}
                    <div>
                        Все подшипники
                    </div></div>
                    {this.state.open ? (
                    <ul className="menu">
                        <li className="menu-item">
                            Подшипник #1
                        </li>
                        <li className="menu-item">
                            Подшипник #2
                        </li>
                    </ul>
                    ) : null}
                </div>
                
            </div>
            </div>

        </div>
    }
}

export default ExgausterComponent;