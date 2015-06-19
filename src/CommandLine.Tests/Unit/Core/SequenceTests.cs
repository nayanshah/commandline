﻿// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandLine.Core;
using CommandLine.Infrastructure;

using Xunit;
using FluentAssertions;

namespace CommandLine.Tests.Unit.Core
{
    public class SequenceTests
    {
        [Fact]
        public void Partition_sequence_values_from_empty_token_sequence()
        {
            var expected = new Token[] { };

            var result = Sequence.Partition(
                new Token[] { },
                name =>
                    new[] { "seq" }.Contains(name)
                        ? Maybe.Just(TypeDescriptor.Create(TypeDescriptorKind.Sequence, Maybe.Nothing<int>()))
                        : Maybe.Nothing<TypeDescriptor>());

            expected.ShouldAllBeEquivalentTo(result);
        }

        [Fact]
        public void Partition_sequence_values()
        {
            var expected = new[]
                {
                    Token.Name("seq"), Token.Value("seqval0"), Token.Value("seqval1")
                };

            var result = Sequence.Partition(
                new[]
                    {
                        Token.Name("str"), Token.Value("strvalue"), Token.Value("freevalue"),
                        Token.Name("seq"), Token.Value("seqval0"), Token.Value("seqval1"),
                        Token.Name("x"), Token.Value("freevalue2")
                    },
                name =>
                    new[] { "seq" }.Contains(name)
                        ? Maybe.Just(TypeDescriptor.Create(TypeDescriptorKind.Sequence, Maybe.Nothing<int>()))
                        : Maybe.Nothing<TypeDescriptor>());

            expected.ShouldAllBeEquivalentTo(result);
        }
    }
}