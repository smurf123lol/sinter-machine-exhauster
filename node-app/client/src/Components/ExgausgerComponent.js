import React from "react";
import  "../css/exgauster.css"
import svg_exg from '../img/exgauster.svg'
class ExgausterComponent extends React.Component{
    constructor(props){
        super();
        this.props = props;
    }

    render(){
        return <div className="exgauster-wrapper">
            <div><div className="exgauster-head">
                <div className="ex-blink"></div>
                {this.props.data.ex_name}
                <button className="exgauster-button">></button>
            </div></div>
            <div className="exgauster-container">
                <img src={svg_exg} alt="Эксгаустер"></img>
                <div><h1></h1></div>
            </div>

        </div>
    }
}
export default ExgausterComponent;