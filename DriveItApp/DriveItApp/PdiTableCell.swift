//
//  PdiTableCell.swift
//  DriveItApp
//
//  Created by Shaheen Sharifian on 10/16/16.
//  Copyright © 2016 Shaheen Sharifian. All rights reserved.
//

import UIKit

class PdiTableCell: UITableViewCell {

    @IBOutlet weak var nationalAvgLabel: UILabel!
    @IBOutlet weak var userValueLabel: UILabel!
    @IBOutlet weak var descriptionLabel: UILabel!
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
