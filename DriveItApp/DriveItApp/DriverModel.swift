//
//  DriverModel.swift
//  DriveItApp
//
//  Created by Shaheen Sharifian on 10/16/16.
//  Copyright © 2016 Shaheen Sharifian. All rights reserved.
//

import Foundation

class DriverModel {
    public var pId : String
    public var carId : Int
    public var value : Double
    
    init(pId : String, carId : Int, value : Double) {
        self.pId = pId
        self.carId = carId
        self.value = value
    }
}
