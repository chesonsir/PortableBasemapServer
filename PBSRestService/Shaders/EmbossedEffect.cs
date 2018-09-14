﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace PBS.Shaders {
    /// <summary>
    /// This is the implementation of an extensible framework ShaderEffect which loads
    /// a shader model 2 pixel shader. Dependecy properties declared in this class are mapped
    /// to registers as defined in the *.ps file being loaded below.
    /// </summary>
    internal class EmbossedEffect : ShaderEffect {
        // Brush-valued properties turn into sampler-property in the shader.
        // This helper sets "ImplicitInput" as the default, meaning the default
        // sampler is whatever the rendering of the element it's being applied to is.

        /// <summary>
        /// The explict input for this pixel shader.
        /// </summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(EmbossedEffect), 0, SamplingMode.Bilinear);

        /// <summary>
        /// This property is mapped to the Amount variable within the pixel shader. 
        /// </summary>
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(double), typeof(EmbossedEffect), new PropertyMetadata(3d, PixelShaderConstantCallback(0)));

        /// <summary>
        /// This property is mapped to the Width variable within the pixel shader. 
        /// </summary>
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(EmbossedEffect), new PropertyMetadata(0.0006d, PixelShaderConstantCallback(1)));

        /// <summary>
        /// Creates an instance and updates the shader's variables to the default values.
        /// </summary>
        public EmbossedEffect(double amount) {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("/PBS;component/Shaders/Embossed.ps", UriKind.Relative);
            this.PixelShader = pixelShader;
            this.Amount = amount;
            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
            UpdateShaderValue(WidthProperty);
        }

        /// <summary>
        /// Gets or sets the Input shader sampler.
        /// </summary>
        [BrowsableAttribute(false)]
        public Brush Input {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Amount variable within the shader.
        /// </summary>
        public double Amount {
            get { return (double)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Width variable within the shader.
        /// </summary>
        public double Width {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }
    }
}
