#!/bin/bash
#$1 is the first parameter being passed when calling the script. The variable filename will be used to refer to this.
input=$1
output="$1.new"

#Reorder
awk 'BEGIN {FS=" "; OFS=" "} {print $1, $3, $2}' $input > $output

#Replace string
sed -i 's/private /</g' $output
sed -i 's/;//g' $output
sed -i 's/System.Windows.Forms.Button/Object="Button" Visible="True" Enabled="False"\/>/g' $output
sed -i 's/System.Windows.Forms.CheckBox/Object="CheckBox" Visible="True" Enabled="False"\/>/g' $output
sed -i 's/System.Windows.Forms.ComboBox/Object="ComboBox" Visible="True" Enabled="False"\/>/g' $output
sed -i 's/System.Windows.Forms.GroupBox/Object="GroupBox" Visible="True" Enabled="False"\/>/g' $output
sed -i 's/System.Windows.Forms.Label/Object="Label" Visible="True" Enabled="False"\/>/g' $output
sed -i 's/System.Windows.Forms.TextBox/Object="TextBox" Visible="True" Enabled="False" ReadOnly="True"\/>/g' $output

